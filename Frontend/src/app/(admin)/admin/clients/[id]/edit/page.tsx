"use client";

import { useState, useEffect } from "react";
import { useRouter, useParams } from "next/navigation";
import { EditClient } from "@/components/users/EditClient";

interface User {
  id: number;
  companyId: number;
  name: string;
  email: string;
  role: string;
}

interface Company {
  id: number;
  name: string;
}

const API = `${process.env.NEXT_PUBLIC_APP_URL}/api`;

export default function EditClientPage() {
  const router = useRouter();
  const params = useParams();
  const userId = Number(params.id);

  const [user, setUser] = useState<User | null>(null);
  const [companies, setCompanies] = useState<Company[]>([]);

  const [formData, setFormData] = useState({
    companyId: 0,
    name: "",
    email: "",
    role: "user",
  });

  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);

        const [userRes, companiesRes] = await Promise.all([
          fetch(`${API}/user/${userId}`),
          fetch(`${API}/company`),
        ]);

        if (!userRes.ok) throw new Error("Błąd pobierania użytkownika");
        if (!companiesRes.ok) throw new Error("Błąd pobierania firm");

        const userData = await userRes.json();
        const companiesData = await companiesRes.json();

        setUser(userData);
        setCompanies(companiesData);

        setFormData({
          companyId: userData.companyId ?? 0,
          name: userData.name ?? "",
          email: userData.email ?? "",
          role: userData.role ?? "user",
        });
      } catch (err: any) {
        setError(err.message || "Nieznany błąd");
        setUser(null);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [userId]);

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, type, value } = e.target;

    setFormData((prev) => ({
      ...prev,
      [name]: type === "number" ? Number(value) : value,
    }));
  };

  const handleRoleChange = async (newRole: string) => {
    setFormData((prev) => ({ ...prev, role: newRole }));

    try {
      await fetch(`${API}/user/${userId}/role`, {
        method: "PATCH",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newRole),
      });

      setUser((prev) => (prev ? { ...prev, role: newRole } : prev));
    } catch (err) {
      console.error("Nie udało się zmienić roli:", err);
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!user) return;

    try {
      const payload = {
        UserId: user.id,
        Name: formData.name || "",
        CompanyId: Number(formData.companyId) || 1,
        Email: formData.email || "",
        Role: formData.role === "admin" ? "Admin" : "User",
      };

      const res = await fetch(`${API}/user/${userId}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload),
      });

      if (!res.ok) {
        const text = await res.text();
        throw new Error(`Błąd ${res.status}: ${text}`);
      }

      router.push("/admin/clients");
    } catch (err: any) {
      setError(err.message || "Nie udało się zaktualizować użytkownika");
    }
  };

  if (loading)
    return <p className="mt-16 text-gray-500">Ładowanie użytkownika...</p>;
  if (error) return <p className="mt-16 text-red-500">Błąd: {error}</p>;
  if (!user) return <div className="mt-16">User not found</div>;

  return (
    <div className="mt-16 max-w-2xl">
      <h1 className="text-2xl font-bold mb-6">Edit Client</h1>

      <EditClient
        formData={formData}
        companies={companies}
        onChange={handleChange}
        onRoleChange={handleRoleChange}
        onSubmit={handleSubmit}
        onCancel={() => router.push("/admin/clients")}
      />
    </div>
  );
}