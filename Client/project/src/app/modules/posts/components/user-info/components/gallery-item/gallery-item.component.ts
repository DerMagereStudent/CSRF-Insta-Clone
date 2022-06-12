import { Component, Input, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-gallery-item',
  templateUrl: './gallery-item.component.html',
  styleUrls: ['./gallery-item.component.scss']
})
export class GalleryItemComponent implements OnInit {
  @Input() public post: any;
  public imageUrl: string = '';

  constructor() {
    this.imageUrl = environment.apiRoutes.posts.image
  }

  ngOnInit() {
  }

}
