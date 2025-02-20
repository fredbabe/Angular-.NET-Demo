import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { MealDayDTO } from '../../../api/generated/stp-client';

@Component({
  selector: 'meal-card',
  imports: [CommonModule],
  templateUrl: './meal-card.component.html',
  styleUrl: './meal-card.component.scss',
})
export class MealCardComponent {
  @Input() meal: MealDayDTO | undefined;
}
