 //LUDecomposition_OpenMP.cpp : This file contains the 'main' function. Program execution begins and ends there.


#include<string>
#include<iostream>
#include<vector>
#include<omp.h>
#define ld long double
#define n 2000
#include<sys/types.h>
#include<time.h>
#include<stdlib.h>



using namespace std;

ld a[n][n], lwr[n][n], upr[n][n];
int main() {
	int i, j, k, nthreads, tid;

	// mat init
	srand(time(NULL));
	for (i = 0; i < n; i++)
	{
		for (j = 0; j < n; j++)
		{
			a[i][j] = 1 + rand();
		}
	}

	// Parallel
	int b1 = 5;

	#pragma omp parallel shared(lwr,upr,a,nthreads) private(tid,i,k,j)
	{
		tid = omp_get_thread_num();
		if (tid == 0) {
			nthreads = omp_get_num_threads();
			cout << nthreads << " threads" << endl;
		}

		for (k = 0; k < n; k++)
		{
			lwr[k][k] = 1;
			#pragma omp for 
			for (j = k; j < n; j++)
			{
				ld sum = 0;
				for (int s = 0; s <= k - 1; s++)
				{
					sum += lwr[k][s] * upr[s][j];
				}
				upr[k][j] = a[k][j] - sum;
			}
			#pragma omp for 
			for (i = k + 1; i < n; i++)
			{
				ld sum = 0;
				for (int s = 0; s <= k - 1; s++)
				{
					sum += lwr[i][s] * upr[s][k];
				}
				lwr[i][k] = (a[i][k] - sum) / upr[k][k];
			}
		}
	}



	printf("Start  Seq\n");

	// seq
	int b2 = 5;

	for (k = 0; k < n; k++)
	{
		lwr[k][k] = 1;
		for (j = k; j < n; j++)
		{
			ld sum = 0;
			for (int s = 0; s <= k - 1; s++)
			{
				sum += lwr[k][s] * upr[s][j];
			}
			upr[k][j] = a[k][j] - sum;
		}
		for (i = k + 1; i < n; i++)
		{
			ld sum = 0;
			for (int s = 0; s <= k - 1; s++)
			{
				sum += lwr[i][s] * upr[s][k];
			}
			lwr[i][k] = (a[i][k] - sum) / upr[k][k];
		}
	}

	int b3 = 5;
 }