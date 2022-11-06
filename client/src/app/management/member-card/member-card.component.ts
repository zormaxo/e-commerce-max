import { Component, OnInit, Input } from '@angular/core';
import { Member } from 'src/app/shared/models/member';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.scss'],
})
export class MemberCardComponent implements OnInit {
  @Input() member: Member;

  constructor() {}

  ngOnInit(): void {}
}
