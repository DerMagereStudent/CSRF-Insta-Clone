import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostsComponent } from './posts.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { UserInfoComponent } from './components/user-info/user-info.component';
import { CookieModule } from 'ngx-cookie';
import { SharedModule } from '../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PostsRoutingModule } from './posts-routing.module';
import { PostService } from './services/post.service';
import { UserService } from './services/user.service';
import { GalleryItemComponent } from './components/user-info/components/gallery-item/gallery-item.component';

@NgModule({
  imports: [
    CommonModule,
    CookieModule.withOptions(),
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    PostsRoutingModule
  ],
  declarations: [
    PostsComponent,
    HomePageComponent,
    UserInfoComponent,
    GalleryItemComponent
  ],
  providers: [
    PostService,
    UserService
  ]
})
export class PostsModule { }
