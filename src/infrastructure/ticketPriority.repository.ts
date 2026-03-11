import { PrismaClient } from "@prisma/client";
import { TicketPriority } from "@/domain/ticketPriority/ticketPriority.entity";
import { ITicketPriorityRepository } from "@/domain/ticketPriority/ticketPriority.irepository";

export class TicketPriorityRepository implements ITicketPriorityRepository {
  constructor(private prisma: PrismaClient) {}

  async findByName(name: string): Promise<TicketPriority | null> {
    return await this.prisma.ticketPriority.findUnique({ where: { name } });
  }

  async findAll(): Promise<TicketPriority[]> {
    return await this.prisma.ticketPriority.findMany();
  }

  async create(data: Omit<TicketPriority, "id">): Promise<TicketPriority> {
    return await this.prisma.ticketPriority.create({ data: { ...data } });
  }

  async update(id: string, data: Partial<Omit<TicketPriority, "id">>): Promise<TicketPriority> {
    return await this.prisma.ticketPriority.update({ where: { id }, data: data });
  }

  async delete(id: string): Promise<void> {
    await this.prisma.ticketPriority.delete({ where: { id } });
  }
}
