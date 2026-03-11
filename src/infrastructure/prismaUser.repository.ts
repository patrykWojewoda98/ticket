import { PrismaClient } from "@prisma/client";
import { User } from "@/domain/user/user.entity";
import { UserRepository } from "@/domain/user/user.irepository";

export class PrismaUserRepository implements UserRepository {
  constructor(private prisma: PrismaClient) {}

  async findByRole(role: string): Promise<User[]> {
    return await this.prisma.user.findMany({ where: { role } });
  }

  async setUserRole(userId: string, role: string): Promise<User> {
    return await this.prisma.user.update({ where: { id: userId }, data: { role } });
  }
}
