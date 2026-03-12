import { z } from "zod";

export const CompanySchema = z.object({
  name: z.string().min(2, "Nazwa musi mieć co najmniej 2 znaki.").max(100, "Nazwa może mieć maksymalnie 100 znaków.").trim(),
  email: z.email("Wprowadź poprawny format adresu e-mail.").min(3, "Adres e-mail musi mieć co najmniej 3 znaki.").max(255, "Adres e-mail może mieć maksymalnie 255 znaków.").toLowerCase().trim(),
  phoneNumber: z.string().min(5, "Numer telefonu musi mieć co najmniej 5 znaków.").max(20, "Numer telefonu może mieć maksymalnie 20 znaków.").regex(/^[0-9+()-\s]*$/, "Numer telefonu zawiera nieprawidłowe znaki.").trim(),
  address: z.string().min(5, "Adres musi mieć co najmniej 5 znaków.").max(200, "Adres może mieć maksymalnie 200 znaków.").trim(),
});

export type CompanyInput = z.infer<typeof CompanySchema>;
