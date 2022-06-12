import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from '../../shared/services/http.service';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private httpService: HttpService) { }

  public async getHomePagePosts(): Promise<any> {
    return this.httpService.get(environment.apiRoutes.posts.homePage);
  }

  public async getUserPosts(userId?: string): Promise<any> {
    return this.httpService.get(environment.apiRoutes.posts.post + (userId == null ? "" : `?UserId=${userId}`));
  }

  public async postImages(description: string, imageIds: string[]): Promise<any> {
    return this.httpService.post(environment.apiRoutes.posts.post, {
      description: description,
      imageIds: imageIds
    });
  }
}
