import { Component, Input, OnInit, numberAttribute } from '@angular/core';
import { CourseService } from '../../services/course.service';
import { Course, CourseCreateDto, CourseUpdateDto } from '../../models/course.model';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { EntityViewComponent } from '../../../core/components/entity-view/entity-view.component';
import { UserRoles } from '../../../users/models/user.model';
import moment from 'moment';

@Component({
	selector: 'se-course',
	templateUrl: './course.component.html',
	styleUrls: ['./course.component.scss'],
})
export class CourseComponent extends EntityViewComponent<Course, CourseCreateDto, CourseUpdateDto> {
	UserRoles = UserRoles;

	moment = moment;

	constructor(
		public override authentication: AuthenticationService,
		protected override entityService: CourseService,
	) {
		super(authentication, entityService)
	}

	stringToColor(str: string): string {
		let hash = 0;
		for (var i = 0; i < str.length; i++) {
			hash = str.charCodeAt(i) + ((hash << 15) - hash);
		}
		let colour = '#';
		for (let i = 0; i < 3; i++) {
			let value = (hash >> (i * 8)) & 0xFF;
			colour += ('00' + value.toString(16)).substr(-2);
		}
		return colour;
	}
}