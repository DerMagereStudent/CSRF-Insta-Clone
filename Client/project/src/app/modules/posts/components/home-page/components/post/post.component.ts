import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { PostService } from 'src/app/modules/posts/services/post.service';
import { UserService } from 'src/app/modules/posts/services/user.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss']
})
export class PostComponent implements OnInit {
  private _post: any;

  @ViewChild('detailDialog') public detailDialog!: ElementRef;
  @Input() public set post(value: any) {
    this._post = value;
    this.liked = undefined;
    this.checkLiked();
    this.getUsername();
    this.userService.checkFollow(this._post.userId).then(response => this.isFollowing = response.content.following);
  }

  public get post(): any { return this._post; }
  public username: string = '';
  public liked?: boolean;
  public isFollowing?: boolean;

  public imageUrl: string = '';

  constructor(private postService: PostService, private userService: UserService) {
    this.imageUrl = environment.apiRoutes.posts.image
  }

  ngOnInit() {
  }

  public checkLiked(): void {
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

  public async followUnfollow(): Promise<void> {
    if (this.isFollowing == null)
      return;

    if (!this.isFollowing) {
      await this.userService.followUser(this.post.userId!);
      this.isFollowing = true;
      return;
    }

    await this.userService.unfollowUser(this.post.userId!);
    this.isFollowing = false;
    return;
  }

  public async getUsername(): Promise<void> {
    await this.userService.getUserProfile(this._post.userId).then(response => this.username = response.content.profile.displayName);
  }
}
