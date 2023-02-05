import { Component, OnInit } from '@angular/core';
import { MessageService } from '../../core/services/message.service';
import { Message } from '../../shared/models/message';
import { Pagination2 } from '../../shared/models/pagination2';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.scss'],
})
export class MessagesComponent implements OnInit {
  messages?: Message[];
  pagination?: Pagination2;
  container = 'Unread';
  pageNumber = 1;
  pageSize = 5;
  loading = false;

  constructor(private messageService: MessageService) {}

  ngOnInit(): void {
    this.loadMessages();
  }

  loadMessages() {
    this.loading = true;
    this.messageService.getMessages(this.pageNumber, this.pageSize, this.container).subscribe({
      next: (response: any) => {
        this.messages = response.result.result;
        this.pagination = response.pagination;
        this.loading = false;
      },
    });
  }

  deleteMessage(id: number) {
    this.messageService.deleteMessage(id).subscribe({
      next: () =>
        this.messages?.splice(
          this.messages.findIndex((m) => m.id === id),
          1
        ),
    });
  }

  pageChanged(event: any) {
    if (this.pageNumber !== event.page) {
      this.pageNumber = event.page;
      this.loadMessages();
    }
  }
}
