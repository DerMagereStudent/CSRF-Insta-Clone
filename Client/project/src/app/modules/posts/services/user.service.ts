import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from '../../shared/services/http.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpService: HttpService) { }

  public async getUserProfile(userId?: string): Promise<any> {
    return this.httpService.get(environment.apiRoutes.user.userProfile + (userId == null ? "" : `?UserId=${userId}`));
  }

  public async updateBiography(biography: string): Promise<void> {
    return this.httpService.put(environment.apiRoutes.user.biography, { Biography: biography });
  }
}
