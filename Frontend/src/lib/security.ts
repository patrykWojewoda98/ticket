/**
 * Narzędzia bezpieczeństwa dla walidacji i sanityzacji danych
 * Pomagają zapobiegać XSS, atakom injection i innym zagrożeniom bezpieczeństwa
 */

import DOMPurify from 'isomorphic-dompurify';

/**
 * Sanityzuje dane HTML, aby zapobiec atakom XSS
 */
export function sanitizeHtml(input: string): string {
  if (!input || typeof input !== 'string') return '';
  return DOMPurify.sanitize(input, {
    ALLOWED_TAGS: [],
    ALLOWED_ATTR: [],
  });
}

/**
 * Waliduje format email
 */
export function isValidEmail(email: string): boolean {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return emailRegex.test(email) && email.length <= 254;
}

/**
 * Waliduje i sanityzuje dane wejściowe dla operacji bazodanowych
 */
export function sanitizeInput(input: string, maxLength: number = 1000): string {
  if (!input || typeof input !== 'string') return '';

  let sanitized = input.trim();
  sanitized = sanitized.replace(/[<>'"&]/g, '');
  if (sanitized.length > maxLength) {
    sanitized = sanitized.substring(0, maxLength);
  }
  return sanitized;
}

/**
 * Waliduje siłę hasła
 */
export function isValidPassword(password: string): { valid: boolean; errors: string[] } {
  const errors: string[] = [];

  if (password.length < 8) {
    errors.push('Hasło musi mieć co najmniej 8 znaków');
  }

  if (!/[A-Z]/.test(password)) {
    errors.push('Hasło musi zawierać wielką literę');
  }

  if (!/[a-z]/.test(password)) {
    errors.push('Hasło musi zawierać małą literę');
  }

  if (!/\d/.test(password)) {
    errors.push('Hasło musi zawierać cyfrę');
  }

  if (!/[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(password)) {
    errors.push('Hasło musi zawierać znak specjalny');
  }

  return {
    valid: errors.length === 0,
    errors,
  };
}

/**
 * Waliduje imię/nazwisko (dozwolone litery, spacje, myślniki, apostrofy)
 */
export function isValidName(name: string): boolean {
  const nameRegex = /^[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ\s\-']+$/;
  return nameRegex.test(name) && name.length >= 2 && name.length <= 100;
}

/**
 * Klasa pomocnicza do limitowania liczby prób
 */
class RateLimiter {
  private attempts: Map<string, number[]> = new Map();

  isAllowed(key: string, maxAttempts: number = 5, windowMs: number = 60000): boolean {
    const now = Date.now();
    const attempts = this.attempts.get(key) || [];

    const validAttempts = attempts.filter(time => now - time < windowMs);

    if (validAttempts.length >= maxAttempts) {
      return false;
    }

    validAttempts.push(now);
    this.attempts.set(key, validAttempts);
    return true;
  }

  reset(key: string): void {
    this.attempts.delete(key);
  }
}

export const rateLimiter = new RateLimiter();

/**
 * Bezpieczna klasa do przechowywania danych w localStorage
 */
export class SecureStorage {
  private static instance: SecureStorage;
  private prefix = 'secure_';

  static getInstance(): SecureStorage {
    if (!SecureStorage.instance) {
      SecureStorage.instance = new SecureStorage();
    }
    return SecureStorage.instance;
  }

  setItem(key: string, value: string): void {
    try {
      const encrypted = btoa(value);
      localStorage.setItem(this.prefix + key, encrypted);
    } catch (error) {
      console.error('Błąd zapisu:', error);
    }
  }

  getItem(key: string): string | null {
    try {
      const encrypted = localStorage.getItem(this.prefix + key);
      return encrypted ? atob(encrypted) : null;
    } catch (error) {
      console.error('Błąd odczytu:', error);
      return null;
    }
  }

  removeItem(key: string): void {
    localStorage.removeItem(this.prefix + key);
  }

  clear(): void {
    Object.keys(localStorage).forEach(key => {
      if (key.startsWith(this.prefix)) {
        localStorage.removeItem(key);
      }
    });
  }
}

export const secureStorage = SecureStorage.getInstance();

/**
 * Generator tokenów CSRF
 */
export function generateCSRFToken(): string {
  const array = new Uint8Array(32);
  crypto.getRandomValues(array);
  return Array.from(array, byte => byte.toString(16).padStart(2, '0')).join('');
}

/**
 * Bezpieczna wersja fetch z timeout i obsługą błędów
 */
export async function secureFetch(
  url: string,
  options: RequestInit = {},
  timeout: number = 10000
): Promise<Response> {
  const controller = new AbortController();
  const timeoutId = setTimeout(() => controller.abort(), timeout);

  try {
    const response = await fetch(url, {
      ...options,
      signal: controller.signal,
      credentials: 'same-origin',
    });

    clearTimeout(timeoutId);
    return response;
  } catch (error) {
    clearTimeout(timeoutId);
    if (error instanceof Error && error.name === 'AbortError') {
      throw new Error('Przekroczono czas oczekiwania');
    }
    throw error;
  }
}