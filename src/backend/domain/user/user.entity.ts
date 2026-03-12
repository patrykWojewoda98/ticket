export interface User {
  id: string;
  companyId?: string | null;
  email: string;
  emailVerified: boolean;
  role: string;
  name: string;
  image?: string | null;
  createdAt: Date;
  updatedAt: Date;
}
