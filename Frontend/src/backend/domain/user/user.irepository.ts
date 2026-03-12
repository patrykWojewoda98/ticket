import { User } from "@/backend/domain/user/user.entity";

export interface IUserRepository {
  findByRole(role: string): Promise<User[]>;
  setUserRole(userId: string, role: string): Promise<User>;
}
