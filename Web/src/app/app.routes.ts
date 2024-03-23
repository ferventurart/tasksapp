import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';

export const configRoutes: Routes = [
    {
        path: '',
        component: HomeComponent,
        title: 'Task App - Home'
    }
];
