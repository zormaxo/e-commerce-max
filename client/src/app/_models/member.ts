import { Photo } from './photo';

export interface Member {
  id: number;
  userName: string;
  firstName: string;
  lastName: string;
  photoUrl: string;
  logoUrl: string;
  phoneNumber: string;
  created: Date;
  lastActive: Date;
  photos: Photo[];
}
