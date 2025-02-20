import { inject, Injectable } from '@angular/core';
import {
  Client,
  MealPlanCreateRequest,
  MealPlanCreateResponse,
} from '../generated/stp-client';
import { lastValueFrom } from 'rxjs';

@Injectable()
export class MealPlanService {
  private client = inject(Client);

  createMealPlan(
    values: MealPlanCreateRequest
  ): Promise<MealPlanCreateResponse> {
    return lastValueFrom(this.client.createMealPlan(values));
  }
}
