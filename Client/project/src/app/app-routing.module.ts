import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path:'identity', loadChildren: () => import('./modules/identity/identity.module').then(m => m.IdentityModule) },
  { path:'posts', loadChildren: () => import('./modules/posts/posts.module').then(m => m.PostsModule) },
  { path: '', redirectTo: 'posts', pathMatch: 'full' },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
