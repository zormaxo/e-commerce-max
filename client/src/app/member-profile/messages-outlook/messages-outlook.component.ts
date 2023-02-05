import { Component, OnInit } from '@angular/core';
import { MessageService } from 'src/app/core/services/message.service';
import { Message } from 'src/app/shared/models/message';
import { Pagination } from 'src/app/shared/models/pagination';
import { Pagination2 } from 'src/app/shared/models/pagination2';

@Component({
  selector: 'app-messages-outlook',
  templateUrl: './messages-outlook.component.html',
  styleUrls: ['./messages-outlook.component.scss'],
})
export class MessagesOutlookComponent implements OnInit {
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
      next: (response) => {
        this.messages = response.result;
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
