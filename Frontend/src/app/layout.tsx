import { ReactNode } from "react";
import { Poppins } from "next/font/google";
<<<<<<< HEAD
import type { Metadata } from "next";
=======
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
import "./globals.css";
import Header from "@/components/common/header";
import Footer from "@/components/common/footer";
import { AuthProvider } from "@/components/common/AuthContext";
<<<<<<< HEAD
import ErrorBoundary from "@/components/common/ErrorBoundary";
=======
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e

const poppins = Poppins({
  subsets: ["latin", "latin-ext"],
  weight: ["400", "700"],
  display: "swap",
});

interface LayoutProps {
  children: ReactNode;
}

<<<<<<< HEAD
export const metadata: Metadata = {
  title: "Ticket System - Secure Support Platform",
  description: "Secure ticket management system for customer support",
  robots: "noindex, nofollow", // Since this appears to be an internal system
  viewport: "width=device-width, initial-scale=1",
  other: {
    "X-Robots-Tag": "noindex, nofollow",
  },
};

export default function Layout({ children }: LayoutProps) {
  return (
    <html lang="pl">
      <head>
        {/* Additional security meta tags */}
        <meta name="referrer" content="strict-origin-when-cross-origin" />
        <meta name="format-detection" content="telephone=no" />
      </head>
      <body className={poppins.className}>
        <ErrorBoundary>
          <AuthProvider>
            <div className="flex flex-col mx-auto px-6 max-w-7xl min-h-screen">
              <Header />
              <main className="flex-grow py-14">{children}</main>
              <Footer />
            </div>
          </AuthProvider>
        </ErrorBoundary>
=======
export default function Layout({ children }: LayoutProps) {
  return (
    <html lang="pl">
      <body className={poppins.className}>
        <AuthProvider>
          <div className="flex flex-col mx-auto px-6 max-w-7xl min-h-screen">
            <Header />
            <main className="flex-grow py-14">{children}</main>
            <Footer />
          </div>
        </AuthProvider>
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
      </body>
    </html>
  );
}
