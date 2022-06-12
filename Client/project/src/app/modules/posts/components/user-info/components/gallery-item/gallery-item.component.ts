import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { PostService } from 'src/app/modules/posts/services/post.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-gallery-item',
  templateUrl: './gallery-item.component.html',
  styleUrls: ['./gallery-item.component.scss']
})
export class GalleryItemComponent implements OnInit {
  private _post: any;

  @ViewChild('detailDialog') public detailDialog!: ElementRef;
  @Input() public set post(value: any) {
    this._post = value;
    this.liked = undefined;
    this.ckeckLiked();
  }

  public get post(): any { return this._post; }
  public liked?: boolean = false;

  public imageUrl: string = '';

  constructor(private postService: PostService) {
    this.imageUrl = environment.apiRoutes.posts.image
  }

  ngOnInit() {
  }

  public ckeckLiked(): void {
    this.postService.checkLike(this.post.id).then((response: any) => {
      this.liked = response.content.postLiked;
      console.log(this.liked);
    });
  }

  public async likeUnlike(): Promise<void> {
    if (this.liked == null)
      return;

    if (!this.liked) {
      await this.postService.likePost(this.post.id)
      this.liked = true;
      this.post.likes++;
      return;
    }

    await this.postService.unlikePost(this.post.id)
    this.liked = false;
    this.post.likes--;
    return;
  }
}
