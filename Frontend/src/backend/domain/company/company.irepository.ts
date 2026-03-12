import { Company } from "@/backend/domain/company/company.entity";

export interface ICompanyRepository {
  create(data: Omit<Company, "id" | "createdAt" | "updatedAt">): Promise<Company>;
  update(id: string, data: Partial<Omit<Company, "id" | "createdAt" | "updatedAt">>): Promise<Company>;
  delete(id: string): Promise<void>;
}
