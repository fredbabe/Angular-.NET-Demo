import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import {
  provideTanStackQuery,
  QueryClient,
} from '@tanstack/angular-query-experimental';
import { CompanyService } from '../api/services/company-service';
import { API_BASE_URL, Client } from '../api/generated/stp-client';
import { MealPlanService } from '../api/services/meal-plan-service';
import { provideLottieOptions } from 'ngx-lottie';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    provideTanStackQuery(new QueryClient()),
    CompanyService,
    MealPlanService,
    Client,
    provideLottieOptions({
      player: () => import('lottie-web'),
    }),
    { provide: API_BASE_URL, useValue: 'https://localhost:7041' },
    provideAnimationsAsync(), // Provide your actual API base URL
  ],
};
