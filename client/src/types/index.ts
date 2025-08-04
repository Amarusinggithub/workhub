import { type LucideIcon } from 'lucide-react';

export type AuthField = 'firstName' | 'lastName' | 'email' | 'password' | 'confirmPassword' | 'auth';
export const USERDATA_STORAGE_KEY = 'userData';

export type AuthErrorType =
	| 'This field cannot be empty'
	| 'invalid-credentials'
	| 'user-not-found'
	| 'password and confirm password must match'
	| 'unknown';

export type AuthErrorCode = `${AuthField}:${AuthErrorType}`;

export interface User {
	id: number;
	email: string;
	name?: string;
	firstName: string;
	lastName: string;
	avatarUrl?: string;
	headerImageUrl?: string;
	jobTItle?: string;
	password?: string;
	organization?: string;
	location?: string;
	updatedAt?: Date;
	createdAt?: Date;
	lastLoggedIn?: Date;
	isActive?: boolean;
	emailVerifiedAt?: Date | null;
	[key: string]: unknown;
}

export interface Auth {
	user: User;
}

export interface BreadcrumbItem {
	title: string;
	href: string;
}

export interface NavGroup {
	title: string;
	items: NavItem[];
}

export interface NavItem {
	title: string;
	href: string;
	icon?: LucideIcon | null;
	isActive?: boolean;
}

export interface SharedData {
	name: string;
	quote: { message: string; author: string };
	auth: Auth;
	sidebarOpen: boolean;
	[key: string]: unknown;
}

type OAuthAccount = {};

type WorkSpace = {};

type Project = {};

type Category = {};

type Plan = {};

type Notification = {};

type Task = {};

type Project = {};

type Resource = {};

type Role = {};

type Offer = {};
