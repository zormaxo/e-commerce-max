import { Photo } from './photo';

export interface Member {
  id: number;
  username: string;
  photoUrl: string;
  logoUrl: string;
  created: Date;
}
