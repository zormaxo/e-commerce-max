import { Component, Input, OnInit } from '@angular/core';
import { PresenceService } from 'src/app/core/services/presence.service';
import { Member } from 'src/app/shared/models/member';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css'],
})
export class MemberCardComponent implements OnInit {
  @Input() member: Member | undefined;

  constructor(public presenceService: PresenceService) {}

  ngOnInit(): void {}
}
