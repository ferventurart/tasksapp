import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { configRoutes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { provideStore } from '@ngrx/store';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(configRoutes),
    provideHttpClient(),
    provideStore()
]
};
