import { Component, computed, effect, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SelectFieldComponent } from '../shared/components/select-field/select-field.component';
import { MultipleTagFieldComponent } from '../shared/components/multiple-tag-field/multiple-tag-field.component';
import {
  injectMutation,
  injectQuery,
  QueryClient,
} from '@tanstack/angular-query-experimental';
import { CompanyService } from '../api/services/company-service';
import { MealPlanService } from '../api/services/meal-plan-service';
import {
  MealPlanCreateRequest,
  MealPlanCreateResponse,
  MealType,
} from '../api/generated/stp-client';
import { SubmitButtonComponent } from '../shared/components/submit-button/submit-button.component';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AnimationOptions, LottieDirective } from 'ngx-lottie';
import { AnimationItem } from 'lottie-web';
import { CommonModule } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MealCardComponent } from '../features/meal-card/meal-card/meal-card.component';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    SelectFieldComponent,
    MultipleTagFieldComponent,
    SubmitButtonComponent,
    ReactiveFormsModule,
    LottieDirective,
    CommonModule,
    MatProgressSpinnerModule,
    MealCardComponent,
  ],
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  options: AnimationOptions = {
    path: 'assets/animations/cookingv2.json', // Correct path from assets
  };

  constructor() {
    effect(() => {
      this.isLoadingData.set(this.getCompaniesQuery.isFetching());
    });
  }

  // Form
  mealPlanForm = new FormGroup({
    amountOfDays: new FormControl<number | null>(null, [Validators.required]),
    mealType: new FormControl<MealType | null>(null, [Validators.required]),
    amountOfPeople: new FormControl<number | null>(null, [Validators.required]),
    superMarketId: new FormControl<string | null>(null, [Validators.required]),
    preferedIngredients: new FormControl<string[]>([]),
    freshnessPreference: new FormControl<string>(''),
  });

  // Services
  companyService = inject(CompanyService);
  mealPlanService = inject(MealPlanService);
  queryClient = inject(QueryClient);

  // Variables
  selectedPreference: string | undefined;
  amountOfPeople: string | undefined;
  supermarket: string | undefined;
  amountOfDays: string | undefined;

  // Signals
  isSubmitting = signal(false);
  isLoadingData = signal(false);
  mealPlan = signal<MealPlanCreateResponse | undefined>(undefined);

  // Queries
  getCompaniesQuery = injectQuery(() => ({
    queryKey: ['companies'],
    queryFn: () => this.companyService.getCompanies(),
  }));

  createMealPlanQuery = injectMutation(() => ({
    queryKey: ['mealPlan'],
    mutationFn: (mealPlanValues: MealPlanCreateRequest) =>
      this.mealPlanService.createMealPlan(mealPlanValues),
    onSuccess: (response) => {
      this.queryClient.invalidateQueries({ queryKey: ['mealPlan'] });
      this.isSubmitting.set(false);
      this.mealPlan.set(response);
    },
    onMutate: () => {
      this.isSubmitting.set(true);
    },
    onError: () => {
      this.isSubmitting.set(false);
    },
  }));

  // Functions
  animationCreated(animationItem: AnimationItem): void {}

  onPreferenceChange(value: string) {
    this.selectedPreference = value;
  }

  onAmountOfPeopleChange(value: string) {
    this.amountOfPeople = value;
  }

  onSupermarketChange(value: string) {
    this.supermarket = value;
  }

  onAmountOfDaysChange(value: string) {
    this.amountOfDays = value;
  }

  async createMealPlan() {
    if (this.mealPlanForm.invalid) {
      this.mealPlanForm.markAllAsTouched();
      return;
    }

    const formValue = this.mealPlanForm.getRawValue();

    const mealPlanRequest: MealPlanCreateRequest = {
      days: Number(formValue.amountOfDays),
      amountOfPeople: Number(formValue.amountOfPeople),
      mealType: Number(formValue.mealType),
      superMarketId: formValue.superMarketId!,
      freshnessPreference: formValue.freshnessPreference || '',
      preferedIngredients: formValue.preferedIngredients || [],
    };

    await this.createMealPlanQuery.mutateAsync(mealPlanRequest);
  }

  generateOptions = (
    items: (string | number)[]
  ): { name: string; value: string }[] => {
    return items.map((item) => ({
      name: item.toString(),
      value: item.toString(),
    }));
  };

  dietaryPreferences = [
    { name: 'Normal', value: MealType._0.toString() },
    { name: 'Vegetar', value: MealType._1.toString() },
    { name: 'Mixed', value: MealType._2.toString() },
  ];
  amountOfPeopleOptions = this.generateOptions([1, 2, 3, 4]);
  amountOfDaysOptions = this.generateOptions([1, 2, 3, 4, 5, 6, 7]);

  supermarkets = computed(() => {
    const data = this.getCompaniesQuery.data();
    if (data) {
      return data.map((x) => ({
        name: x.name,
        value: x.id,
      }));
    }
    return [];
  });
}
