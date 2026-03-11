import { PrismaClient } from "@prisma/client";
import { Company } from "@/domain/company/company.entity";
import { ICompanyRepository } from "@/domain/company/company.irepository";

export class CompanyRepository implements ICompanyRepository {
  constructor(private prisma: PrismaClient) {}

  async create(data: Omit<Company, "id" | "createdAt" | "updatedAt">): Promise<Company> {
    return await this.prisma.company.create({ data: { ...data } });
  }

  async update(id: string, data: Partial<Omit<Company, "id" | "createdAt" | "updatedAt">>): Promise<Company> {
    return await this.prisma.company.update({ where: { id }, data: data });
  }

  async delete(id: string): Promise<void> {
    await this.prisma.company.delete({ where: { id } });
  }
}
