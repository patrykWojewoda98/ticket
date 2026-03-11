import { PrismaClient } from "@prisma/client";
import { TicketCategory } from "@/domain/ticketCategory/ticketCategory.entity";
import { ITicketCategoryRepository } from "@/domain/ticketCategory/ticketCategory.irepository";

export class TicketNotificationRepository implements ITicketCategoryRepository {
  constructor(private prisma: PrismaClient) {}

  async findByName(name: string): Promise<TicketCategory | null> {
    return await this.prisma.ticketCategory.findUnique({ where: { name } });
  }

  async findAll(): Promise<TicketCategory[]> {
    return await this.prisma.ticketCategory.findMany({ orderBy: { name: "asc" } });
  }

  async create(data: Omit<TicketCategory, "id">): Promise<TicketCategory> {
    return await this.prisma.ticketCategory.create({ data: { ...data } });
  }

  async update(id: string, data: Partial<Omit<TicketCategory, "id">>): Promise<TicketCategory> {
    return await this.prisma.ticketCategory.update({ where: { id }, data });
  }

  async delete(id: string): Promise<void> {
    await this.prisma.ticketCategory.delete({ where: { id } });
  }
}
