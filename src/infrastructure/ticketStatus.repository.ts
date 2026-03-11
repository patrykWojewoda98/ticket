import { PrismaClient } from "@prisma/client";
import { TicketStatus } from "@/domain/ticketStatus/ticketStatus.entity";
import { ITicketStatusRepository } from "@/domain/ticketStatus/ticketStatus.irepository";

export class TicketStatusRepository implements ITicketStatusRepository {
  constructor(private prisma: PrismaClient) {}

  async findByName(name: string): Promise<TicketStatus | null> {
    return await this.prisma.ticketStatus.findUnique({ where: { name } });
  }

  async findAll(): Promise<TicketStatus[]> {
    return await this.prisma.ticketStatus.findMany({ orderBy: { name: "asc" } });
  }

  async create(data: Omit<TicketStatus, "id">): Promise<TicketStatus> {
    return await this.prisma.ticketStatus.create({ data: { name: data.name } });
  }

  async update(id: string, data: Partial<Omit<TicketStatus, "id">>): Promise<TicketStatus> {
    return await this.prisma.ticketStatus.update({ where: { id }, data: data });
  }

  async delete(id: string): Promise<void> {
    await this.prisma.ticketStatus.delete({ where: { id } });
  }
}
