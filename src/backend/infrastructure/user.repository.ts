import { PrismaClient } from "@prisma/client";
import { User } from "@/backend/domain/user/user.entity";
import { IUserRepository } from "@/backend/domain/user/user.irepository";

export class UserRepository implements IUserRepository {
  constructor(private prisma: PrismaClient) {}

  async findByRole(role: string): Promise<User[]> {
    return await this.prisma.user.findMany({ where: { role } });
  }

  async setUserRole(userId: string, role: string): Promise<User> {
    return await this.prisma.user.update({ where: { id: userId }, data: { role } });
  }
}
