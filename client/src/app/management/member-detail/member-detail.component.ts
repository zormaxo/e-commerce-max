import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from '@kolkov/ngx-gallery';
import { MembersService } from 'src/app/_services/members.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.scss'],
})
export class MemberDetailComponent implements OnInit {
  member: Member;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor(private memberService: MembersService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadMember();

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false,
      },
    ];
  }

  getImages(): NgxGalleryImage[] {
    const imageUrls = [];
    imageUrls.push({
      small: this.member.photoUrl,
      medium: this.member.photoUrl,
      big: this.member.photoUrl,
    });
    return imageUrls;
  }

  loadMember() {
    this.memberService.getMember(+this.route.snapshot.paramMap.get('userId')).subscribe((member: any) => {
      this.member = member.result;
      this.galleryImages = this.getImages();
    });
  }
}
