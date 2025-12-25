pipeline {
	agent any

	stages {
		stage('Checkout') {
			steps {
				checkout scm
			}
		}

		stage('Build Configuration') {
			steps {
				echo 'Building Microservices...'
				sh 'ls -la'
			}
		}

		stage('Docker Build') {
			steps {
				echo 'Starting Docker Build...'
			}
		}

		stage('Deploy') {
			steps {
				echo 'Deploying Application...'
			}
		}
	}
}
