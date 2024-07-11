import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatIcon } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatMenuModule } from '@angular/material/menu';

import { PlanesRoutingModule } from './planes-routing.module';
import { PlanePage } from './pages/plane-page/plane.page';
import { PlaneListPage } from './pages/plane-list-page/plane-list.page';
import { PlaneComponent } from './components/plane/plane.component';
import { CreatePlanePage } from './pages/create-plane-page/create-plane.page';
import { MatOptionModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';


@NgModule({
  imports: [
    PlanesRoutingModule,
		MatProgressSpinnerModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    MatInputModule,
    MatOptionModule,
    MatFormFieldModule,
    MatButtonModule,
    MatCardModule,
    MatIcon,
    MatLabel,
    MatSelectModule,
    MatMenuModule,
  ],
  declarations: [
    PlaneComponent,
    PlanePage,
    PlaneListPage,
    CreatePlanePage,
  ],
  exports: [
    PlaneComponent,
    PlanePage,
    PlaneListPage,
    CreatePlanePage,
  ],
	providers: [
		MatDatepickerModule,
	]
})
export class PlanesModule { }
