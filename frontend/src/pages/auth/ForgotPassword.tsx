import { LoaderCircle } from 'lucide-react';
import { FormEventHandler, useState, useState } from 'react';
import {useState} from 'react'
import InputError from '../../components/input-error';
import TextLink from '../../components/text-link';
import { Button } from '../../components/ui/button';
import { Input } from '../../components/ui/input';
import { Label } from '../../components/ui/label';
import AuthLayout from '../../layouts/auth-layout';

type ForgotPasswordProps={
    status?:string;
}

export default function ForgotPassword({ status }: ForgotPasswordProps) {
    const[form,setForm]=useState<string>("");
    const[isLoading,setLoading]=useState<boolean>(false);
    const[error,setError]=useState(null);

    const submit: FormEventHandler = (e) => {
        e.preventDefault();

    };

    return (
        <AuthLayout title="Forgot password" description="Enter your email to receive a password reset link">
            <h1 title="Forgot password" />

            {status && <div className="mb-4 text-center text-sm font-medium text-green-600">{status}</div>}

            <div className="space-y-6">
                <form onSubmit={submit}>
                    <div className="grid gap-2">
                        <Label htmlFor="email">Email address</Label>
                        <Input
                            id="email"
                            type="email"
                            name="email"
                            autoComplete="off"
                            value={form.email}
                            autoFocus
                            onChange={(e) => setForm('email', e.target.value)}
                            placeholder="email@example.com"
                        />

                        <InputError message={error.email} />
                    </div>

                    <div className="my-6 flex items-center justify-start">
                        <Button className="w-full" disabled={isLoading}>
                            {isLoading && <LoaderCircle className="h-4 w-4 animate-spin" />}
                            Email password reset link
                        </Button>
                    </div>
                </form>

                <div className="space-x-1 text-center text-sm text-muted-foreground">
                    <span>Or, return to</span>
                    <TextLink href={}>log in</TextLink>
                </div>
            </div>
        </AuthLayout>
    );
}
