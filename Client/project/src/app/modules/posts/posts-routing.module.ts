import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserLoggedInGuard } from '../shared/guards/user-logged-in-guard.service';
import { HomePageComponent } from './components/home-page/home-page.component';
import { UserInfoComponent } from './components/user-info/user-info.component';
import { PostsComponent } from './posts.component';

const routes: Routes = [
  { path: '', component: PostsComponent, children: [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: "home", component: HomePageComponent, canActivate: [UserLoggedInGuard]},
    { path: "user", component: UserInfoComponent, canActivate: [UserLoggedInGuard]},
    { path: "user/:UserId", component: UserInfoComponent, canActivate: [UserLoggedInGuard]},
  ]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PostsRoutingModule { }
