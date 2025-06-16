//import { usePage } from '@inertiajs/react';
import { SharedData } from '../types/index';
import { SidebarProvider } from './ui/sidebar';

interface AppShellProps {
	children: React.ReactNode;
	variant?: 'header' | 'sidebar';
}

export function AppShell({ children, variant = 'header' }: AppShellProps) {
	//const isOpen = usePage<SharedData>().props.sidebarOpen;
    const isOpen =false;
	if (variant === 'header') {
		return <div className="flex min-h-screen w-full flex-col">{children}</div>;
	}

	return <SidebarProvider defaultOpen={isOpen}>{children}</SidebarProvider>;
}
