import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FlexLayoutModule } from '@angular/flex-layout';

import {AutoCompleteModule} from 'primeng/autocomplete';
import {ButtonModule} from 'primeng/button';
import {CalendarModule} from 'primeng/calendar';
import {CardModule} from 'primeng/card';
import {ContextMenuModule} from 'primeng/contextmenu';
import {DialogModule} from 'primeng/dialog';
import {DropdownModule} from 'primeng/dropdown';
import {FileUploadModule} from 'primeng/fileupload';
import {InputTextModule} from 'primeng/inputtext';
import {MultiSelectModule} from 'primeng/multiselect';
import {PanelModule} from 'primeng/panel';
import {ProgressBarModule} from 'primeng/progressbar';
import {SliderModule} from 'primeng/slider';
import {TableModule} from 'primeng/table';
import {ToastModule} from 'primeng/toast';
import {ToggleButtonModule} from 'primeng/togglebutton';
import { HttpClientModule } from '@angular/common/http';
import { HttpService } from './services/http.service';
import { IdentityService } from './services/identity.service';
import { MessageService } from 'primeng/api';
import { UserLoggedInGuard } from './guards/user-logged-in-guard.service';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,    
    MatToolbarModule,
    FlexLayoutModule,

    AutoCompleteModule,
    ButtonModule,
    CalendarModule,
    CardModule,
    ContextMenuModule,
    DialogModule,
    DropdownModule,
    FileUploadModule,
    InputTextModule,
    MultiSelectModule,
    PanelModule,
    ProgressBarModule,
    SliderModule,
    TableModule,
    ToastModule,
    ToggleButtonModule,
  ],
  exports: [ 
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,    
    MatToolbarModule,
    FlexLayoutModule,
    
    AutoCompleteModule,
    ButtonModule,
    CalendarModule,
    CardModule,
    ContextMenuModule,
    DialogModule,
    DropdownModule,
    FileUploadModule,
    InputTextModule,
    MultiSelectModule,
    PanelModule,
    ProgressBarModule,
    SliderModule,
    TableModule,
    ToastModule,
    ToggleButtonModule
  ],
  declarations: [],
  providers: [
    HttpService,
    IdentityService,
    MessageService,
    UserLoggedInGuard
  ]
})
export class SharedModule { }
