import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie';
import { environment } from 'src/environments/environment';
import { IdentityService } from '../shared/services/identity.service';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss']
})
export class PostsComponent implements OnInit {
  public userSuggestions: any[] = [];
  public selectedUser: any;

  constructor(private identityService: IdentityService, private cookieService: CookieService, private router: Router) {}

  ngOnInit() {
  }

  public async search(event: any): Promise<void> {
    var response = await this.identityService.sendGetUsersByNameOrEmailRequest(event.query);
    this.userSuggestions = response.content.users;
  }

  public openUser(): void {
    if (!this.selectedUser || typeof this.selectedUser === 'string')
      return;

    this.router.navigateByUrl(`/posts/user/${this.selectedUser.id}`);
  }

  public logout(): void {
    this.cookieService.remove(environment.authTokenHeaderKey);
    window.location.reload();
  }
}
