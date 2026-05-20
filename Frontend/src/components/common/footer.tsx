"use client";

import Link from "next/link";
import { Facebook, Twitter, Instagram } from "lucide-react";
import { Button } from "@/components/ui/button";

const SOCIAL_LINKS = [
  { icon: Facebook, label: "Facebook", href: "#" },
  { icon: Twitter, label: "Twitter", href: "#" },
  { icon: Instagram, label: "Instagram", href: "#" },
] as const;

export default function Footer() {
  const currentYear = new Date().getFullYear();

  return (
    <footer className="bg-background py-6 border-t w-full">
      <div className="flex md:flex-row flex-col justify-between items-center gap-4 mx-auto px-6 container">
        <p className="text-muted-foreground text-sm">&copy; {currentYear} Wszelkie prawa zastrzeżone.</p>

        <nav className="flex items-center gap-1">
          {SOCIAL_LINKS.map(({ icon: Icon, label, href }) => (
            <Button key={label} variant="ghost" size="icon" className="rounded-full" asChild>
              <Link href={href} target="_blank" rel="noreferrer" aria-label={label}>
                <Icon className="opacity-80 hover:opacity-100 w-[1.2rem] h-[1.2rem] transition-opacity" />
              </Link>
            </Button>
          ))}
        </nav>
      </div>
    </footer>
  );
}
