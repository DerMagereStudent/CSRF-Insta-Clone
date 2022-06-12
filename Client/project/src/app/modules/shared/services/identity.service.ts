import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from './http.service';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {
  constructor(private httpService: HttpService) { }

  public async sendSignUpRequest(body: { username: string, email: string, password: string }): Promise<any> {
    return this.httpService.post(environment.apiRoutes.identity.signUp, body);
  }

  public async sendLoginRequest(body: { usernameEmail: string, password: string }): Promise<any> {
    return this.httpService.post(environment.apiRoutes.identity.login, body);
  }

  public async sendAuthorizeUserRequest(body: { token: string}): Promise<any> {
    return this.httpService.post(environment.apiRoutes.identity.authorizeUser, body);
  }

  public async sendGetUserByIdRequest(userId: string): Promise<any> {
    return this.httpService.get(environment.apiRoutes.identity.informationUserById+`?UserId=${userId}`)
  }

  public async sendGetUsersByNameOrEmailRequest(nameEmail: string): Promise<any> {
    return this.httpService.get(environment.apiRoutes.identity.informationUsersByNameOrEmail+`?UsernameEmail=${nameEmail}`)
  }
}