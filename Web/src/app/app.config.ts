import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { configRoutes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { provideState, provideStore } from '@ngrx/store';
import { filterReducer, filterFeatureKey } from './store/filter.reducer';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(configRoutes),
    provideHttpClient(),
    provideStore(),
    provideState({ name: filterFeatureKey, reducer: filterReducer })
]
};
