import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie';
import { environment } from 'src/environments/environment';
import { IdentityService } from '../services/identity.service';

@Injectable({
  providedIn: 'root'
})
export class UserLoggedInGuard implements CanActivate {
  constructor(private router: Router, private identityService: IdentityService, private cookieService: CookieService) { }

  public async canActivate(): Promise<boolean> {
    var authToken = this.cookieService.get(environment.authTokenHeaderKey);

    if (authToken == null) {
      this.router.navigateByUrl('/identity/login');
      return false;
    }

    try {
      var response = await this.identityService.sendAuthorizeUserRequest({ token: authToken });

      const authorized = response.content != undefined && response.content.authorized != undefined && response.content.authorized;

      if (!authorized)
        this.router.navigateByUrl('/identity/login');
      
      return authorized;
    }
    catch (err) {
      this.router.navigateByUrl('/identity/login');
      return false;
    };
  }
}