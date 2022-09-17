import { Photo } from "./photo";

export interface Member {
  id: number;
  username: string;
  firstName: string;
  surname: string;
  photoUrl: string;
  logoUrl: string;
  phoneNumber: string;
  created: Date;
  lastActive: Date;
  photos: Photo[];
}
