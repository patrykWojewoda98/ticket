export type UserRole = "ADMIN" | "USER";

export type User = {
  companyName?: string;
  email: string;
  emailVerified: boolean;
  role: UserRole;
  name: string;
  image?: string;
};

export const mockUsers: User[] = [
  {
    companyName: "Acme Corp",
    email: "john.doe@acme.com",
    emailVerified: true,
    role: "ADMIN",
    name: "John Doe",
    image: "https://randomuser.me/api/portraits/men/1.jpg",
  },
  {
    companyName: "TechSoft",
    email: "jane.smith@techsoft.com",
    emailVerified: false,
    role: "USER",
    name: "Jane Smith",
    image: "https://randomuser.me/api/portraits/women/2.jpg",
  },
  {
    companyName: "InnovateX",
    email: "bob.johnson@innovatex.com",
    emailVerified: true,
    role: "USER",
    name: "Bob Johnson",
    image: "https://randomuser.me/api/portraits/men/3.jpg",
  },
  {
    companyName: "Global Solutions",
    email: "alice.williams@globalsolutions.com",
    emailVerified: false,
    role: "USER",
    name: "Alice Williams",
    image: "https://randomuser.me/api/portraits/women/4.jpg",
  },
  {
    companyName: "NextGen Labs",
    email: "michael.brown@nextgenlabs.com",
    emailVerified: true,
    role: "ADMIN",
    name: "Michael Brown",
    image: "https://randomuser.me/api/portraits/men/5.jpg",
  },
];