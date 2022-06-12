import { Component, OnInit } from '@angular/core';
import { PostService } from '../../services/post.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {

  public posts: any[] = [];

  constructor(private postService: PostService) { }

  async ngOnInit(): Promise<void> {
    const response = await this.postService.getHomePagePosts();
    this.posts = response.content.posts;
    console.log(this.posts);
  }

}
