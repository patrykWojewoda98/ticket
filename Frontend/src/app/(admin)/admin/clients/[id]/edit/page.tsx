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

const API = `${process.env.NEXT_PUBLIC_APP_URL}/api/user`;

export default function EditClientPage() {
  const router = useRouter();
  const params = useParams();
  const userId = Number(params.id);

  const [user, setUser] = useState<User | null>(null);
  const [formData, setFormData] = useState({
    companyId: 0,
    name: "",
    email: "",
    emailVerified: false,
    role: "user",
  });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchUser = async () => {
      try {
        setLoading(true);
        const res = await fetch(`${API}/${userId}`);
        if (!res.ok) throw new Error(`Błąd ${res.status}`);
        const data = await res.json();

        setUser(data);
        setFormData({
          companyId: data.companyId ?? 0,
          name: data.name ?? "",
          email: data.email ?? "",
          emailVerified: !!data.emailVerified,
          role: data.role ?? "user",
        });
      } catch (err: any) {
        setError(err.message || "Nieznany błąd");
        setUser(null);
      } finally {
        setLoading(false);
      }
    };

    fetchUser();
  }, [userId]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, type, value, checked } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: type === "checkbox" ? checked : type === "number" ? Number(value) : value,
    }));
  };

  const handleRoleChange = async (newRole: string) => {
    setFormData((prev) => ({ ...prev, role: newRole }));
    try {
      await fetch(`${API}/${userId}/role`, {
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
        EmailVerified: !!formData.emailVerified,
        Role: formData.role === "admin" ? "Admin" : "User",
      };

      const res = await fetch(`${API}/${userId}`, {
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

  if (loading) return <p className="mt-16 text-gray-500">Ładowanie użytkownika...</p>;
  if (error) return <p className="mt-16 text-red-500">Błąd: {error}</p>;
  if (!user) return <div className="mt-16">User not found</div>;

  return (
    <div className="mt-16 max-w-2xl">
      <h1 className="text-2xl font-bold mb-6">Edit Client</h1>
      <EditClient
        formData={formData}
        onChange={handleChange}
        onRoleChange={handleRoleChange}
        onSubmit={handleSubmit}
        onCancel={() => router.push("/admin/clients")}
      />
    </div>
  );
}