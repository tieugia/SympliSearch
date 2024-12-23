import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: 'search', loadChildren: () => import('./features/search/search.module').then(m => m.SearchModule) },
  { path: '**', redirectTo: 'search' },
];