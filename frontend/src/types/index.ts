import { type LucideIcon } from 'lucide-react';
export interface User {
	id: number;
	email: string;
	name: string;

	FirstName: string;
	LastName: string;
	ProfilePicture?: string;
	HeaderImage?: string;
	JobTItle?: string;
	Email: string;
	password?: string;
	Organization?: string;
	Location?: string;
	UpdatedAt: Date;
	CreatedAt: Date;
	LastLoggedIn: Date;
	IsActive: boolean;
	EmailVerifiedAt: Date | null;
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

type UserGroUp = {};
