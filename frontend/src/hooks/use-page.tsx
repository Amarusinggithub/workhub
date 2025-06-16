import React, { createContext, useContext, useState, type PropsWithChildren } from 'react';
import type { SharedData } from '../types';

type PageContextType = {
	sharedData: SharedData | undefined;
	url: string;
	isCheckingAuth: boolean;
	setUrl: React.Dispatch<React.SetStateAction<string>>;
	setIsCheckingAuth: React.Dispatch<React.SetStateAction<boolean>>;
	setSharedData: React.Dispatch<React.SetStateAction<SharedData | undefined>>;
};

type PageProviderProps = PropsWithChildren;

const PageContext = createContext<PageContextType | undefined>(undefined);

export function AuthProvider({ children }: PageProviderProps) {
	const [url, setUrl] = useState<string>('');
	const [isCheckingAuth, setIsCheckingAuth] = useState<boolean>(false);
	const [sharedData, setSharedData] = useState<SharedData | undefined>();

	return (
		<PageContext.Provider value={{ url, setUrl, isCheckingAuth, setIsCheckingAuth, sharedData, setSharedData }}>{children}</PageContext.Provider>
	);
}

export default function usePage() {
	const context = useContext(PageContext);
	if (!context) {
		throw new Error('usePage must be used within the context of PageProvider');
	}
	return context;
}
