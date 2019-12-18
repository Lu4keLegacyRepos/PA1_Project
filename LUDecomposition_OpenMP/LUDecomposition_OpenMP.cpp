// LUDecomposition_OpenMP.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include<string>
#include<iostream>
#include<vector>
#include<omp.h>
#define ld long double
#define n 2000
#include<sys/types.h>
#include<time.h>
#include<stdlib.h>

omp_lock_t simple_lock;

int main() {
	omp_init_lock(&simple_lock);

#pragma omp parallel num_threads(4)
	{
		int tid = omp_get_thread_num();

		while (!omp_test_lock(&simple_lock))
			printf_s("Thread %d - failed to acquire simple_lock\n",
				tid);

		printf_s("Thread %d - acquired simple_lock\n", tid);

		printf_s("Thread %d - released simple_lock\n", tid);
		omp_unset_lock(&simple_lock);
	}

	omp_destroy_lock(&simple_lock);
}


//using namespace std;
//
//ld a[n][n], lwr[n][n], upr[n][n];
//int main() {
//	int i, j, k, nthreads, tid;
//
//	// mat init
//	srand(time(NULL));
//	for (i = 0; i < n; i++)
//	{
//		for (j = 0; j < n; j++)
//		{
//			a[i][j] = 1 + rand();
//		}
//	}
//
//	// Parallel
//	int b1 = 5;
//
//	#pragma omp parallel num_threads(12) shared(lwr,upr,a,nthreads) private(tid,i,k,j)
//	{
//		tid = omp_get_thread_num();
//		if (tid == 0) {
//			nthreads = omp_get_num_threads();
//			cout << nthreads << " threads" << endl;
//		}
//
//		for (k = 0; k < n; k++)
//		{
//			lwr[k][k] = 1;
//			#pragma omp for 
//			for (j = k; j < n; j++)
//			{
//				ld sum = 0;
//				for (int s = 0; s <= k - 1; s++)
//				{
//					sum += lwr[k][s] * upr[s][j];
//				}
//				upr[k][j] = a[k][j] - sum;
//			}
//			#pragma omp for 
//			for (i = k + 1; i < n; i++)
//			{
//				ld sum = 0;
//				for (int s = 0; s <= k - 1; s++)
//				{
//					sum += lwr[i][s] * upr[s][k];
//				}
//				lwr[i][k] = (a[i][k] - sum) / upr[k][k];
//			}
//		}
//	}
//
//
//
//	printf("Start  Seq\n");
//
//	// seq
//	int b2 = 5;
//
//	for (k = 0; k < n; k++)
//	{
//		lwr[k][k] = 1;
//		for (j = k; j < n; j++)
//		{
//			ld sum = 0;
//			for (int s = 0; s <= k - 1; s++)
//			{
//				sum += lwr[k][s] * upr[s][j];
//			}
//			upr[k][j] = a[k][j] - sum;
//		}
//		for (i = k + 1; i < n; i++)
//		{
//			ld sum = 0;
//			for (int s = 0; s <= k - 1; s++)
//			{
//				sum += lwr[i][s] * upr[s][k];
//			}
//			lwr[i][k] = (a[i][k] - sum) / upr[k][k];
//		}
//	}
//
//	int b3 = 5;
// }