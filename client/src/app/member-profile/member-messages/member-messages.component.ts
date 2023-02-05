import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { take } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { MessageService } from 'src/app/core/services/message.service';
import { Member } from 'src/app/shared/models/member';
import { User } from 'src/app/shared/models/user';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.scss'],
})
export class MemberMessagesComponent implements OnInit, OnDestroy {
  @ViewChild('messageForm') messageForm?: NgForm;
  member: Member = {} as Member;
  user?: User;
  messageContent = '';
  loading = false;

  constructor(
    private accountService: AccountService,
    public messageService: MessageService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: (user) => {
        if (user) this.user = user;
      },
    });
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  ngOnInit(): void {
    this.route.data.subscribe({
      next: (data) => {
        this.member = data['member'];
        this.messageService.createHubConnection(this.user, this.member.email);
      },
    });
  }

  sendMessage() {
    if (!this.member) return;
    this.loading = true;
    this.messageService
      .sendMessage(this.member.email, this.messageContent)
      .then(() => {
        this.messageForm?.reset();
      })
      .finally(() => (this.loading = false));
  }

  ngOnDestroy(): void {
    this.messageService.stopHubConnection();
  }
}
