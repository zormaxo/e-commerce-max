export interface User {
  firstName: string;
  userName: string;
  userId: number;
  token: string;
  photoUrl: string;
  roles: string[];
}

export interface Address {
  firstName: string;
  lastName: string;
  street: string;
  city: string;
  state: string;
  zipCode: string;
}
