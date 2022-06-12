import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private httpClient: HttpClient, private messageService: MessageService) { }

  public async get(url: string, headers?: HttpHeaders): Promise<any> {
    return this.httpClient.get(url, { headers: headers, withCredentials: true}).toPromise().then(this.onRequestSucceeded.bind(this), this.onRequestFailed.bind(this));
  }

  public async post(url: string, body: any, headers?: HttpHeaders): Promise<any> {
    return this.httpClient.post(url, body, {headers: headers, withCredentials: true}).toPromise().then(this.onRequestSucceeded.bind(this), this.onRequestFailed.bind(this));
  }

  public async put(url: string, body: any, headers?: HttpHeaders): Promise<any> {
    return this.httpClient.put(url, body, {headers: headers, withCredentials: true}).toPromise().then(this.onRequestSucceeded.bind(this), this.onRequestFailed.bind(this));
  }

  public async delete(url: string, body: any, headers?: HttpHeaders): Promise<any> {
    return this.httpClient.delete(url, {body: body, headers: headers, withCredentials: true}).toPromise().then(this.onRequestSucceeded.bind(this), this.onRequestFailed.bind(this));
  }

  private onRequestSucceeded(response: any): any {
    if (response.succeeded) {
      response.messages.forEach((message: any) => {
        //console.log(`${message.code}: ${message.description}`);
        //this.messageService.add({severity: "success", summary: message.code, detail: message.description, life: 5000});
      });
    } else {
      response.errors.forEach((error: any) => {
        console.log(`${error.code}: ${error.description}`);
        this.messageService.add({severity: "error", summary: error.code, detail: error.description, life: 5000});
      });
    }

    return response;
  }

  private onRequestFailed(error: any): any {
    if (!(error instanceof HttpErrorResponse)) {
      console.error(error);
      return error;
    }

    if (error.error.errors && error.error.errors instanceof Array) {
      error.error.errors.forEach((error: any) => {
        console.error(`${error.code}: ${error.description}`);
        this.messageService.add({severity: "error", summary: error.code, detail: error.description, life: 5000});
      });
    }

    return error;
  }
}
