import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import jwtDecode from "jwt-decode";
import { CookieService } from 'ngx-cookie';
import { environment } from 'src/environments/environment';
import { PostService } from '../../services/post.service';
import { UserService } from '../../services/user.service';
import { FileUpload } from 'primeng/fileupload'

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent implements OnInit {
  @ViewChild('biographyInput', { static: false }) public biographyInput!: ElementRef<HTMLInputElement>;
  @ViewChild('descriptionInput', { static: false }) public descriptionInput!: ElementRef<HTMLInputElement>;
  @ViewChild('imagesFileUpload', { static: false }) public imagesFileUpload!: ElementRef<FileUpload>;

  public profile: any;
  public isOwnProfile: boolean = false;
  public userId?: string;

  public isEditingBiography: boolean = false;
  public isFollowing?: boolean;

  public posts: any[] = [];

  public description: string = "";
  public uploadedFiles: { id: string, file: any }[] = [];

  public imageUrl: string = "";

  constructor(private activatedRoute: ActivatedRoute, private postService: PostService, private userService: UserService, private cookieService: CookieService, private ref: ChangeDetectorRef) {
    this.imageUrl = environment.apiRoutes.posts.image
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.setIsOwnProfile(params);

      if (!this.isOwnProfile)
        this.userId = params.UserId;
      
      this.userService.getUserProfile(this.isOwnProfile ? null : params.UserId).then(response => {
        this.profile = response.content.profile
      });

      this.postService.getUserPosts(this.isOwnProfile ? null : params.UserId).then(response => {
        this.posts = response.content.posts.filter((post: any) => post.images.length > 0)
      });

      if (!this.isOwnProfile) {
        this.userService.checkFollow(this.userId!).then(response => this.isFollowing = response.content.following);
      }
    });
  }

  public editBiography(): void {
    this.isEditingBiography = true;
  }

  public async saveBiography(): Promise<void> {
    this.ref.detectChanges();

    if (!this.biographyInput) {
      this.isEditingBiography = false;
      return;
    }

    const value = this.biographyInput.nativeElement.value;
    await this.userService.updateBiography(value).then(() => this.profile.biography = value);
    this.isEditingBiography = false;
  }

  public cancelEdit(): void {
    this.isEditingBiography = false;
  }

  public onUpload(event: any): void {
    console.log(event);

    for (let i = 0; i < event.files.length; i++)
      this.uploadedFiles.push({ id: event.originalEvent.body.content.imageIds[i], file: event.files[i] });
  }

  public resetUpload(): void {
    this.description = "";
    this.uploadedFiles = [];
    this.imagesFileUpload.nativeElement.clear();
  }

  public async postImages(): Promise<void> {
    if (this.uploadedFiles.length === 0)
      return;

    await this.postService.postImages(this.description, this.uploadedFiles.map(f => f.id));
    window.location.reload();
  }

  public async followUnfollow(): Promise<void> {
    if (this.isOwnProfile || this.isFollowing == null)
      return;

    if (!this.isFollowing) {
      await this.userService.followUser(this.userId!);
      this.isFollowing = true;
      return;
    }

    await this.userService.unfollowUser(this.userId!);
    this.isFollowing = false;
    return;
  }

  private setIsOwnProfile(queryParams: any) {
    if (!queryParams.UserId) {
      this.isOwnProfile = true;
      return;
    }

    const token = this.cookieService.get(environment.authTokenHeaderKey);

    if (token == null) {
      this.isOwnProfile = true;
      return;
    }

    const jwt = jwtDecode(token, undefined) as any;

    if (jwt == null) {
      this.isOwnProfile = true;
      return;
    }

    this.isOwnProfile = jwt.sub === queryParams.UserId;
  }
}
